import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/WeatherForecasts';
import Dropdown from 'react-dropdown';
import 'react-dropdown/style.css';

class FetchData extends Component {
    componentWillMount() {
        // This method runs when the component is first added to the page
        const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
        this.props.requestWeatherForecasts(startDateIndex);
    }

    componentDidMount() {
        this.getSales();
        this.getParameters();
    }

    componentWillReceiveProps(nextProps) {
        // This method runs when incoming props (e.g., route params) change
        const startDateIndex = parseInt(nextProps.match.params.startDateIndex, 10) || 0;
        this.props.requestWeatherForecasts(startDateIndex);
    }

    getSales() {
        fetch("http://localhost:5000/api/sales", {
            method: "GET", mode: "no-cors", headers: {
                'Access-Control-Allow-Origin': '*'
            }
        })
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        sales: result
                    });
                }
            );
    }

    getParameters() {
        fetch("http://localhost:5000/api/sales/getfilterparameters", {
            method: "GET"
        })
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        parameters: result
                    });
                }
            );
    }

    render() {
        return (
            <div>
                {renderFilterSection(this.state)}
                <h1>LinnWorks Sales</h1>
                {renderForecastsTable(this.state)}
                {renderPagination(this.props)}
            </div>
        );
    }
}

function renderFilterSection(state) {
    if (state && state.parameters)
    return <div className="filterSection">
        <div>
            <Dropdown options={state.parameters.countries} placeholder="Select an option" />
        </div>
        <div>
            <Dropdown options={state.parameters.itemTypes} placeholder="Select an option" />
        </div>
        <div>
            <Dropdown options={state.parameters.salesChannels} placeholder="Select an option" />
        </div>
        <div>
            <Dropdown options={state.parameters.regions} placeholder="Select an option" />
        </div>
        <div>
            <Dropdown options={state.parameters.orderPriorities} placeholder="Select an option" />
        </div>
    </div>
}

function renderForecastsTable(state) {
    if (state && state.sales)
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>SaleId</th>
                        <th>Region</th>
                        <th>Country</th>
                        <th>ItemType</th>
                        <th>Sales Channel</th>
                        <th>Order Priority</th>
                        <th>Order Date</th>
                        <th>Order ID</th>
                        <th>Ship Date</th>
                        <th>Unit Sold</th>
                        <th>Unit Price</th>
                        <th>Unit Cost</th>
                        <th>Total Revenue</th>
                        <th>Total Cost</th>
                        <th>Total Profit</th>
                    </tr>
                </thead>
                <tbody>
                    {state.sales.map(sale =>
                        <tr key={sale.saleId}>
                            <td>{sale.saleId}</td>
                            <td>{sale.region}</td>
                            <td>{sale.country}</td>
                            <td>{sale.itemType}</td>
                            <td>{sale.salesChannel}</td>
                            <td>{sale.orderPriority}</td>
                            <td>{sale.orderDate}</td>
                            <td>{sale.orderID}</td>
                            <td>{sale.shipDate}</td>
                            <td>{sale.unitSold}</td>
                            <td>{sale.unitPrice}</td>
                            <td>{sale.unitCost}</td>
                            <td>{sale.totalRevenue}</td>
                            <td>{sale.totalCost}</td>
                            <td>{sale.totalProfit}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
}

function renderPagination(props) {
    const prevStartDateIndex = (props.startDateIndex || 0) - 5;
    const nextStartDateIndex = (props.startDateIndex || 0) + 5;

    return <p className='clearfix text-center'>
        <Link className='btn btn-default pull-left' to={`/fetchdata/${prevStartDateIndex}`}>Previous</Link>
        <Link className='btn btn-default pull-right' to={`/fetchdata/${nextStartDateIndex}`}>Next</Link>
        {props.isLoading ? <span>Loading...</span> : []}
    </p>;
}

export default connect(
    state => state.weatherForecasts,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(FetchData);
