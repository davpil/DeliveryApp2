import React from 'react';
import {
        Button, Modal, ModalHeader, ModalBody
        , Form, ModalFooter, FormGroup, Label, Input
       } from 'reactstrap';
import { withRouter } from 'react-router';
import JsonHelper from '../../JsonHelper';


class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            email: '',
            password: '',
            message: {},
            success: false,
            modal: false
        };

        this.toggle = this.toggle.bind(this);
    }

    componentDidUpdate(prevProps) {
        if ((this.props.showModal !== prevProps.showModal)) {
            this.setState({ modal: this.props.showModal });
        }
    } 

    toggle() {
        this.setState(prevState => ({
            modal: !prevState.modal
        }));
    }

    handleClose = (success) => {
        const { getCurrentUser } = this.props;
        this.setState({
            display: false
        },
            () => {
                if (getCurrentUser !== undefined) {
                    getCurrentUser();
                }
            }
        );
    }
    jsonHelper = new JsonHelper();

    validateForm() {
        return this.state.email.length > 0 && this.state.password.length > 0;
    }

    handleSubmit = event => {
        event.preventDefault();
        //const { handleClose } = this.props;
        this.setState({ message: {} });
        let form = document.querySelector('#Login');
        let formJson = this.jsonHelper.formToJson(form);
        fetch('api/Account',
            {
                method: 'Post',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formJson)
            })
            .then(response => {
                return response.text();
            })
            .then(error => {
                this.setState(({ message }) => {
                    let newObj = { ...message, ...error };
                    return { message: newObj, success: true };
                }, () => {
                    this.handleClose(this.state.success);
                    window.location.reload();
                });
            });
    }

    render() {
        return (
            <div>
                <Modal isOpen={this.state.modal} toggle={this.handleClose} className={this.props.className}>
                    <ModalHeader toggle={this.handleClose}>Modal title</ModalHeader>
                    <ModalBody>
                        <Form onSubmit={this.handleSubmit} id='Login'>
                            <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                                <Label for="exampleEmail" className="mr-sm-2">Email</Label>
                                <Input
                                    type="email"
                                    name="Email"
                                    id="exampleEmail"
                                    
                                    />
                            </FormGroup>
                            <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                                <Label for="examplePassword" className="mr-sm-2">Password</Label>
                                <Input
                                    type="password"
                                    name="Password"
                                    id="examplePassword"
                                     />
                            </FormGroup>
                            <Button
                                color="primary"
                              
                                type="submit">
                                Login</Button>
                        </Form>
                        </ModalBody>
                    <ModalFooter>
                        
                        <Button color="secondary" onClick={this.handleClose}>Cancel</Button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}

export default withRouter(Login);