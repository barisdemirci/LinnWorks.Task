import React, { Component } from 'react';
import { connect } from 'react-redux';

class ImportFile extends Component {
    componentWillMount() {

    }

    render() {
        return (
            <div>
                <h1>Import File</h1>
                Select image to upload:
                <input type="file" name="fileToUpload" id="fileToUpload" />
                <input type="submit" value="Upload Image" name="submit" onClick={this.handleClick} />
            </div>
        );
    }

    handleClick = () => {
        let file = document.getElementById("fileToUpload").files[0];

        let formData = new FormData();

        formData.append("photo", file);
        fetch("http://linnworksqueuemicroservice-dev.eu-central-1.elasticbeanstalk.com/api/v1/file/upload", {
            method: "POST", body: formData, mode: "no-cors", headers: {
                'Access-Control-Allow-Origin': '*'
            }
        })
            .then(res => res.json())
            .then(
                (result) => {
                    alert(result);
                },
                (error) => {
                    alert(error);
                }
            );
    }
}

export default connect()(ImportFile);
