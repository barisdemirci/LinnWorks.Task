import React, { Component } from 'react';
import { connect } from 'react-redux';

class ImportFile extends Component {
    componentWillMount() {

    }

    handleClick = () => {

        let file = document.getElementById("fileToUpload").files[0];

        let formData = new FormData();

        formData.append("excel", file);
        fetch("http://linnworksqueuemicroservice-dev.eu-central-1.elasticbeanstalk.com/api/v1/file/upload", {
            method: "POST", body: formData, mode: "no-cors", headers: {
                'Access-Control-Allow-Origin': '*'
            }
        })
            .then(res => res.json())
            .then(data => console.log(JSON.stringify(data))) // JSON-string from `response.json()` call
            .catch(error => console.error(error));
    }

    render() {
        return (
            <div>
                <h1>Import File</h1>
                Select image to upload:
                <form action="http://linnworksqueuemicroservice-dev.eu-central-1.elasticbeanstalk.com/api/v1/file/upload" method="post" encType="multipart/form-data">
                    <input type="file" name="fileToUpload" id="fileToUpload" />
                    <input type="submit" value="Upload Image" name="submit" />
                </form>
            </div>
        );
    }
}

export default connect()(ImportFile);
