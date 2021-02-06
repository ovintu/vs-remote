import React, { Component } from 'react';

export class VisualStudio extends Component {
    static displayName = VisualStudio.name;

    constructor(props) {
        super(props);
    }

    componentDidMount() {
    }

    async getVsInstances() {
        const response = await fetch('visualstudio');
        const data = await response.json();
        //this.setState({ forecasts: data, loading: false });
    }

    render() {
        return (
            <div>
                <h1>Visual Studio</h1>
                <button className="btn btn-primary" onClick={this.getVsInstances}>Get Current Running VS Instances</button>
            </div>
        );
    }
}
