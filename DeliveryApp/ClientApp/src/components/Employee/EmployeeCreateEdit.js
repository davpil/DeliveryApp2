import React, { Component } from 'react';
import JsonHelper from '../JsonHelper';

export default class EmployeeCreateEdit extends Component {
    constructor(props) {
        super(props);

        this.state = {
            logo: 'Create',
            meth: 'post',
            path: 'CreateTrainer',
            loading: true,
            employee: {
                id: '',
                FirstName: '',
                LastName: ''

            },
            message: {
                FirstName: '',
                LastName: ''
            },
            emplId:''
        };
    }

    //componentDidUpdate(prevProps) {
    //    if (this.state.employee.id !== prevProps.emplId) {
    //        this.getEmployeeById(prevProps.emplId);
    //    }
    //}

    componentDidUpdate() {
        if (this.props.emplId !== this.state.emplId) {
            this.getEmployeeById(this.props.emplId);
        }
    }
   
    getEmployeeById = (Id) => {
        fetch('api/Employee/' + Id, { method: 'get' })
            .then(response => { return response.json(); })
            .then(data => {
                this.setState({
                    emplId: Id,
                    employee: data,
                    loading: false,
                    logo: 'Edit',
                    meth: 'put'
                });
            });
    };

    jsonHelper = new JsonHelper();

    handleSave = (e) => {
        e.preventDefault();
        this.setState({ message: {} });
        let form = document.querySelector('#personForm');
        let formJson = this.jsonHelper.formToJson(form);
        fetch('api/Employee/',
            {
                method: this.state.meth,
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formJson)
            })
            .then(
                response => {
                    return response.json();
                })
            .then(error => {
                this.setState(({ message }) => {
                    let newObj = { ...message, ...error };
                    return { message: newObj };
                });
            });
    }

    renderForm(employee) {
        return (
            <form id='personForm'>
                <input type="hidden"
                    name="Id"
                    value={this.state.employee.id} />
                <div>
                    <label>First Name</label>
                    <input key={employee.firstName}
                        id='FirstName'
                        name='FirstName'
                        className="form-control"
                        type='text'
                        defaultValue={employee.firstName} /> 
                </div>
                <div>
                    <label>Last Name</label>
                    <input key={employee.lastName}
                        id='LastName'
                        name='LastName'
                        className="form-control"
                        type='text' defaultValue={employee.lastName} />             
                </div>
                <button className="btn btn-primary"
                    onClick={this.handleSave} >Submit</button>
            </form>
        )
    }

    render() {
        let contents = this.renderForm(this.state.employee);
        return (
            <div>
                {contents}
            </div>
        );
    }
}