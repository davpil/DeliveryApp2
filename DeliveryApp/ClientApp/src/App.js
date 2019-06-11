import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import EmployeeIndex from './components/Employee/EmployeeIndex';
import EmployeeCreateEdit from './components/Employee/EmployeeCreateEdit';
import RoleIndex from './components/Role/RoleIndex';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetch-data' component={FetchData} />
                <Route path='/Employee/EmployeeCreateEdit' component={EmployeeCreateEdit} />
                <Route path='/Employee/EmployeeIndex' component={EmployeeIndex} />
                <Route path='/Role/RoleIndex' component={RoleIndex} />
            </Layout>
        );
    }
}
