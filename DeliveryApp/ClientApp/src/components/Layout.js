import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';
import { NavMenu } from './NavMenu';
import DynamicMenuIndex from './DynamicMenu/DynamicMenuIndex';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
        <div>
                    <NavMenu />
            <Container>
            <Row>
                    <Col xs="3"><DynamicMenuIndex/></Col>
                <Col xs="9">{this.props.children}</Col>   
            </Row>
            </Container>
        </div>
    );
  }
}
//<div>
//    <NavMenu />
//    <Container>
//        {this.props.children}
//    </Container>
//</div>