import React, { Component } from 'react';
import { connect } from 'react-redux';

class ImportFile extends Component {
    componentWillMount() {

    }

    handleClick = () => {

        let file = document.getElementById("fileToUpload").files[0];

        let formData = new FormData();

        formData.append("excel", file);
        fetch("http://localhost:5000/api/importfile/upload", {
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
                <div>
                    Select image to upload:
                    <input type="file" name="fileToUpload" id="fileToUpload" />
                </div>
                <div>
                    <input type="submit" value="Upload Image" name="submit" onClick={this.handleClick} />
                </div>
            </div>
        );
    }
}

export default connect()(ImportFile);