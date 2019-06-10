import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import EmployeeCreateEdit from './EmployeeCreateEdit';

export default class EmployeeIndex extends Component {
    constructor(props) {
        super(props);

        this.state = {
            employees: [],
            loading: true,
            showCreate: false,
            showDetails: false,
            showEdit: false
        }
    }

    componentDidMount() {
        this.getAllEmployees();
    }

    getAllEmployees = () => {
        fetch('api/Employee', { method: 'get' })
            .then(response => { return response.json(); })
            .then(data => {
                this.setState({
                    employees: data,
                    loading: false
                });
            });
    }

    //handleCreateEdit = (id) => {
    //    this.props.history.push("/employee/EmployeeCreateEdit", id);
    //}

    //handleDelete = (id) => {

    //    if (!window.confirm('Are you sure you want to delete this?')) {
    //        return;
    //    }

    //    fetch('api/Bundle/' + id, { method: 'delete' })
    //        .then(data => {
    //            this.setState(
    //                {
    //                    bundles: this.state.bundles.filter((rec) => {
    //                        return rec.Id != id;
    //                    })
    //                });
    //        })
    //        .then((responseJson) => {
    //            this.deleteBundle(id);
    //            this.props.history.push("Index");
    //        });
    //}

    selectedEmployee = (item) => {
        this.setState({ selectedEmployeeId: item.id });
    };

    renderEmployeeTable = (employees) => {
        return (
            <table className='table'>
                <tbody>
                    {employees.map(item => {
                        return (
                            <tr key={item.id}>
                                <td>
                                    <span>{item.firstName}  {item.lastName}</span>
                                    <button type='button' onClick={(id) => this.selectedEmployee(item)}>Edit</button>
                                    <button className="action" >Delete</button>
                                </td>
                            </tr>
                            )
                    })}
                </tbody>
            </table>
            )

    }

    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderEmployeeTable(this.state.employees);
        return (
            <div>
                <Link to="/employee/EmployeeCreateEdit">Create New</Link>
                <div className="row">
                    <div className="col-md-6">
                        <h1>All Employees</h1>
                        {contents}

                    </div>
                    <div className="col-md-6">
                        <EmployeeCreateEdit emplId={this.state.selectedEmployeeId} />
                    </div>
                </div>
            </div>
        )
    }
}
