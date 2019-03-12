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
        fetch("http://localhost:5000/api/importfile/upload", { method: "POST", body: formData })
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        userSectors: result,
                        isLoaded: true
                    });
                },
                (error) => {
                    this.setState({
                        isLoaded: true
                    });
                }
            );
    }
}

export default connect()(ImportFile);
