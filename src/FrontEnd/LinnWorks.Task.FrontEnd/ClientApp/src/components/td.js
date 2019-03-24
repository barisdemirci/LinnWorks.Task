import React, { Component } from 'react';
import Dropdown from 'react-dropdown';

export class TD extends Component {

    constructor(props) {
        super(props);
        this.state = { showEditField: false };
    }

    handleClick = () => {
        if (this.props.editable === false) {
            return;
        }
        this.setState({ showEditField: true });
    }

    onFieldChange = (e) => {
        let value = e !== null ? e.value : '';
        if (!isNaN(new Date(e).valueOf())) {
            value = e;
        }
        else if (e.currentTarget !== undefined) {
            value = e.currentTarget.value;
        }
        this.props.commitData(value);
        this.setState({ showEditField: false });
    }

    renderEditField(currentValue) {
        return <Dropdown onChange={this.onFieldChange} options={this.props.options} value={currentValue.toString()} />;
    }

    render() {
        return (<td onClick={this.handleClick}>
            {this.state.showEditField ? this.renderEditField(this.props.valueId) : this.props.value}
        </td >);
    }
}