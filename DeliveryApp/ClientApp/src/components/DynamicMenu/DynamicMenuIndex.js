import React, { Component } from 'react';
import { Link } from 'react-router-dom';
//import EmployeeIndex from '../Employee/EmployeeIndex';

import './DynamicMenu.css';

export default class DynamicMenu extends Component {
    state = {
        selectedMenu: null,
        oldId: null,
        menu: []
    };

    componentDidMount() {
        this.getMenu();
    }

    getMenu = () => {
        fetch('api/DynamicMenu', { method: "get" })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    menu: data
                },
                    () => {
                        const { menu } = this.state;
                        const newMenu = menu;
                        newMenu.forEach((item) => { item.menuStyle = 'list-group-item '; });
                        this.setState({ menu: newMenu });
                    }
                );
            });
    }

    onMenuSelected = (selectedMenu) => {
        this.setState({ selectedMenu });
    };

    onclick = (id) => {
        const { menu, oldId } = this.state;
        let newmenuList = menu;
        const findindexnew = newmenuList.findIndex(x => x.id === id);
        const findindexold = newmenuList.findIndex(x => x.id === oldId);
        newmenuList[findindexnew].menuStyle = 'list-group-item active';
        if (oldId !== null) {
            newmenuList[findindexold].menuStyle = 'list-group-item';
        }
        if (oldId !== id) {
            this.setState({
                menuList: newmenuList,
                oldId: id
            });
        }
    }

    renderItems(arr) {
        return arr.map(({ id, title, api, icon, menuStyle, active }) => {
            return (
                <Link to={`/${api}/${api}Index`} key={title}>

                    <li className={menuStyle}
                        key={title}
                        onClick={(event) => { this.onMenuSelected(id); this.onclick(id); }}
                    >
                        <div className="icon fa fa-cloud">  </div>
                        <p className="list-group-item-name">{title}</p>
                    </li>
                </Link>
            );
        });
    }

    render() {
        const { menu } = this.state;

        const items = this.renderItems(menu);

        return (
            <div>
                <ul className="item-list list-group">
                    {items}
                </ul>
            </div>
            
        );
    }
}