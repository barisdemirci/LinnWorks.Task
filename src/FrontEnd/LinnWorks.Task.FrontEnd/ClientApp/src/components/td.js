import React, { Component } from 'react';
import Dropdown from 'react-dropdown';
import DatePicker from "react-datepicker";

export class TD extends Component {

    constructor(props) {
        super(props);
        this.state = { showEditField: false };
    }

    handleClick = (e) => {
        if (e.target && e.target.className.indexOf("react-date") > -1) {
            return;
        }
        if (this.props.editable === false) {
            return;
        }

        this.setState({ showEditField: true });
    }

    onFieldChange = (value) => {
        if (value.currentTarget !== undefined) {
            value = value.currentTarget.value;
        }

        if (value !== this.props.value) {
            this.props.commitData(value);
            this.setState({ showEditField: false });
        }
    }

    renderEditField(currentValue) {
        const { type } = this.props;
        if (type === "select") {
            return <Dropdown onChange={(e) => this.onFieldChange(e.value)} options={this.props.options} value={currentValue.toString()} />;
        }
        else if (type === "date") {
            var date = new Date(currentValue);
            return <DatePicker dateFormat="dd.MM.yyyy" onChange={(value) => this.onFieldChange(value.toISOString())} selected={date} />;
        }
        else if (type === "number") {
            return <input type='number' defaultValue={currentValue} onBlur={(value) => this.onFieldChange(value)} />;
        }
    }

    render() {
        const label = this.props.options !== undefined ? this.props.options.find(x => x.value === this.props.value.toString()).label : this.props.value;
        return (<td onClick={this.handleClick}>
            {this.state.showEditField ? this.renderEditField(this.props.value) : label}
        </td >);
    }
}