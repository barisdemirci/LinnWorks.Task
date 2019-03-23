import React, { Component } from 'react';
import { connect } from 'react-redux';

class ImportFile extends Component {
    componentWillMount() {

    }

    handleClick = () => {
        let file = document.getElementById("fileToUpload").files[0];
        if (file === undefined) {
            alert("Please choose a file");
            return;
        }
        let formData = new FormData();
        formData.append("excel", file);
        var ref = this;
        ref.changeEnablement(true);
        fetch("http://linnworksqueuemicroservice-dev.eu-central-1.elasticbeanstalk.com/api/file/upload", {
            method: "POST", body: formData, mode: "no-cors", headers: {
                'Access-Control-Allow-Origin': '*'
            }
        })
            .then(function () {
                ref.changeEnablement(false);
                alert("File is uploaded successfully.");
            })
            .catch(function () {
                ref.changeEnablement(false);
                alert("Something went wrong.");
            });
    }

    changeEnablement(disabled) {
        document.getElementById("fileToUpload").disabled = disabled;
        document.getElementById("uploadButton").disabled = disabled;
        var label = document.getElementById("statusLabel");
        if (disabled) {
            label.style.visibility = "";
        }
        else {
            label.style.visibility = "hidden";
        }
    }

    render() {
        return (
            <div id="uploadSection" className="import">
                <h1>Import File</h1>
                <div className="uploadInput">
                    Select image to upload:
                    <input type="file" name="fileToUpload" id="fileToUpload" />
                </div>
                <div className="uploadButton">
                    <input id="uploadButton" type="submit" value="Upload Image" name="submit" onClick={this.handleClick} />
                </div>
                <label style={{ visibility: "hidden" }} id="statusLabel">The file is uploading...</label>
            </div>
        );
    }
}

export default connect()(ImportFile);