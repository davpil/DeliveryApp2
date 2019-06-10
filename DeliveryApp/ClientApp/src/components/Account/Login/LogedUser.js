import React, { Component } from 'react';
import Login from './Login';
import { withRouter } from 'react-router';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

class LoggedUser extends Component {
    constructor(props) {
        super(props);

        this.state = {
            userName: '',
            show: false,
            showUser: false
        };
    }

    componentDidMount() {
        this.getCurrentUser();
    }

    getCurrentUser = () => {

        fetch('api/Account/GetUserName', { method: "get" })
            .then(response => {
                return response.text();
            })
            .then(data => {
                this.setState({
                    userName: data,
                    show: false
                });
            });
    }

    onSignIn = () => {
        this.setState({ show: true });
    }

    //onSignUp = () => {
    //    this.props.history.push('/Register');
    // }

    onLogout = () => {
        fetch('api/Account/Logout', { method: "get" })
            .then(response => {
                return response.text();
            })
            .then(data => {
                this.setState({
                    userName: ''
                }, () => { window.location.reload(); });
            });
    }


    render() {

        return (
            <div>
                <div>
                    <span style={{ fontWeight: 'bold' }} onClick={this.onSignIn}>Sign In</span>
                </div>
                <div>
                    <span style={{ fontWeight: 'bold' }} onClick={this.onLogout}>Logout</span>
                </div>
                <div>
                    <span style={{ fontWeight: 'bold' }}>{this.state.userName}</span>
                </div>
                <Login
                    showModal={this.state.show}
                    getCurrentUser={this.getCurrentUser}
                />
            </div>
        );
    }
}

export default withRouter(LoggedUser);