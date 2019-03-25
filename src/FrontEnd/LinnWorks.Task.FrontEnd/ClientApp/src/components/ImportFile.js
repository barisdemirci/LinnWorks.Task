import React, { Component } from 'react';
import { QueueApi } from '../api/QueueApi';

export class ImportFile extends Component {
    componentWillMount() {
        this.queueApi = new QueueApi();
    }

    async handleClick(e) {
        e.preventDefault();
        let file = document.getElementById("fileToUpload").files[0];
        if (file === undefined) {
            alert("Please choose a file");
            return;
        }
        let formData = new FormData();
        formData.append("ExcelTask", file);
        var ref = this;
        ref.changeEnablement(true);
        await this.queueApi.Post("file/upload", formData);
        ref.changeEnablement(false);
    }

    changeEnablement(disabled) {
        this.setState({ disabled: disabled });
    }

    render() {
        let disabled = "";
        let visibilityofLabel = "hidden";
        if (this.state !== null && this.state.disabled !== undefined && this.state.disabled) {
            disabled = "disabled";
            visibilityofLabel = "visible";
        }
        return (
            <div id="uploadSection" className="import">
                <h1>Import File</h1>
                <div className="uploadInput">
                    Select image to upload:
                    <input type="file" name="fileToUpload" id="fileToUpload" disabled={disabled} />
                </div>
                <div className="uploadButton">
                    <input id="uploadButton" type="submit" value="Upload Image" name="submit" onClick={this.handleClick.bind(this)} disabled={disabled} />
                </div>
                <label style={{ visibility: visibilityofLabel }} id="statusLabel">The file is uploading...</label>
            </div>
        );
    }
}