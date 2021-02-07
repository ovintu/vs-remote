import React, { Component } from 'react';
import { AgGridColumn, AgGridReact } from 'ag-grid-react';

import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';

export class VisualStudio extends Component {
    static displayName = VisualStudio.name;

    constructor(props) {
        super(props);

        this.state = {
            viualStudioInstances: []
        }

        this.getVsInstances = this.getVsInstances.bind(this);
        this.onSelectionChanged = this.onSelectionChanged.bind(this);
    }

    async getVsInstances() {
        const response = await fetch('visualstudio');
        const serverData = await response.json();
        this.setState({
            viualStudioInstances: serverData
        });
    }

    async onSelectionChanged(sender) {
        let selectedRows = sender.api.getSelectedRows();
        let currentRow = selectedRows[0];
        var route = `/solution/${currentRow["id"]}`;
        const response = await fetch(route);
    }

    render() {
        return (
            <div>
                <h1>Visual Studio</h1>
                <button className="btn btn-primary" onClick={this.getVsInstances}>Get Visual Studio Instances</button>
                <div className="ag-theme-alpine" style={{ height: 200, width: 600, marginTop: 20 }}>
                    <AgGridReact
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
            </div>
        );
    }
}
