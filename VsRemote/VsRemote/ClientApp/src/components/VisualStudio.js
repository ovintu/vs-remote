import React, { Component } from 'react';

export class VisualStudio extends Component {
    static displayName = VisualStudio.name;

    constructor(props) {
        super(props);
        this.state = { viualStudioInstances: [], loading: false };
    }

    componentDidMount() {
    }

    async getVsInstances() {
        this.setState({ viualStudioInstances: [], loading: true });
        const response = await fetch('visualstudio');
        const vsInstances = await response.json();
        this.setState({ viualStudioInstances: vsInstances, loading: false });
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
