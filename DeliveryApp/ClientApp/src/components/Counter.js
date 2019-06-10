import React, { Component } from 'react';
import { Button, FormGroup, FormControl, ControlLabel, Modal } from 'react-bootstrap';
export class Counter extends Component {
  static displayName = Counter.name;

  constructor (props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
  }

  incrementCounter () {
    this.setState({
      currentCount: this.state.currentCount + 1
    });
  }

  render () {
      return (
          <Modal>
        <h1>Counter</h1>

        <p>This is a simple example of a React component.</p>
              <p>This is a simple example of a React component.</p>
              <p>This is a simple example of a React component.</p>
              <p>This is a simple example of a React component.</p>
              <p>This is a simple example of a React component.</p>
        <p>Current count: <strong>{this.state.currentCount}</strong></p>

              <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
          </Modal>
    );
  }
}
