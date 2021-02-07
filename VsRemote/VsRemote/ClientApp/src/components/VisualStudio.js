import React, { Component } from 'react';
import { AgGridColumn, AgGridReact } from 'ag-grid-react';

import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';

export class VisualStudio extends Component {
    static displayName = VisualStudio.name;

    constructor(props) {
        super(props);

        this.state = {
            selectedVsInstanceId: 0,
            viualStudioInstances: [],
            solutionDetails: {},
            currentStatus: ""
        }

        this.getVsInstances = this.getVsInstances.bind(this);
        this.onSelectionChanged = this.onSelectionChanged.bind(this);
        this.buildSolution = this.buildSolution.bind(this);
    }

    async getVsInstances() {
        const response = await fetch('visualstudio');
        const serverData = await response.json();
        this.setState({
            viualStudioInstances: serverData
        });
    }

    async onSelectionChanged(sender) {
        const selectedRows = sender.api.getSelectedRows();
        const currentRow = selectedRows[0];
        const currentMode = currentRow["currentMode"];
        this.setState({ currentStatus: currentMode });
        if (currentMode == "Running") {
            alert("Selected solution is running. Unable to get details while solution is being run")
        } else {
            const id = currentRow["id"];
            this.setState({ selectedVsInstanceId: id });
            
            const route = `/solution/${id}`;
            const response = await fetch(route);
            const serverData = await response.json();
            console.log(serverData);
            this.setState({
                solutionDetails: serverData
            });
        }
    }

    async buildSolution() {
        if (this.state.selectedVsInstanceId == 0) {
            alert("select a grid row");
        }

        if (this.state.currentStatus == "Running") {
            alert("VS selected is running");
            return;
        }

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }
        };

        //alert(this.state.selectedVsInstanceId);
        const route = `/solution/${this.state.selectedVsInstanceId}`;
        
        const response = await fetch(route, requestOptions);   
        const serverData = await response.json();
    }

    render() {
        return (
            <div>
                <h1>Visual Studio</h1>
                <table>
                    <tr>
                        <td><button className="btn btn-primary" onClick={this.getVsInstances}>Get Visual Studio Instances</button></td>
                        <td><button className="btn btn-secondary" onClick={this.buildSolution}>Build</button></td>
                    </tr>
                </table>                
                <div className="ag-theme-alpine" style={{ height: 200, width: 600, marginTop: 20 }}>
                    <AgGridReact
                        displayName={true}
                        rowData={this.state.viualStudioInstances}
                        rowSelection={'single'}
                        onSelectionChanged={this.onSelectionChanged}
                        defaultColDef={{
                            width: 200,
                            sortable: true,
                            resizable: true
                        }}>
                        <AgGridColumn field="id"></AgGridColumn>
                        <AgGridColumn field="vsEdition"></AgGridColumn>
                        <AgGridColumn field="currentMode"></AgGridColumn>
                        <AgGridColumn field="solutionLoaded"></AgGridColumn>
                    </AgGridReact>
                </div>
                <div>
                    <pre>
                        {JSON.stringify(this.state.solutionDetails, null, 2)}
                    </pre>
                </div>
            </div>
        );
    }
}
