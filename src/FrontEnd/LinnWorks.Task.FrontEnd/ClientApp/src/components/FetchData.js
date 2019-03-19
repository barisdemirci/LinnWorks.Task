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
        this.onCountryChange = this.onCountryChange.bind(this);
        this.onRegionChange = this.onRegionChange.bind(this);
        this.onSalesChannelChange = this.onSalesChannelChange.bind(this);
        this.onItemTypeChange = this.onItemTypeChange.bind(this);
        this.onOrderPriorityChange = this.onOrderPriorityChange.bind(this);
        this.getSales = this.getSales.bind(this);
        this.saveChanges = this.saveChanges.bind(this);
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
        var filter = {};
        if (this.state) {
            filter.CountryId = this.state.selectedCountryId;
            filter.RegionId = this.state.selectedRegionId;
            filter.SalesChannelId = this.state.selectedSalesChannelId;
            filter.OrderPriorityId = this.state.selectedOrderPriorityId;
            filter.ItemTypeId = this.state.selectedItemTypeId;
        }
        filter.PageSize = 1000;

        fetch("http://localhost:5000/api/sales", {
            method: "POST", body: JSON.stringify(filter), headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
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

    onCountryChange(e) {
        this.setState({ selectedCountryId: e.value });
    }

    onRegionChange(e) {
        this.setState({ selectedRegionId: e.value });
    }

    onSalesChannelChange(e) {
        this.setState({ selectedSalesChannelId: e.value });
    }

    onItemTypeChange(e) {
        this.setState({ selectedItemTypeId: e.value });
    }

    onOrderPriorityChange(e) {
        this.setState({ selectedOrderPriorityId: e.value });
    }

    saveChanges() {
        fetch("http://localhost:5000/api/sales", {
            method: "PUT", body: JSON.stringify(this.state.sales), headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
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

    render() {
        return (
            <div>
                {renderFilterSection(this.state, this)}
                <h1>LinnWorks Sales</h1>
                {renderSalesTable(this.state)}
                {renderPagination(this.props)}
            </div>
        );
    }
}

function renderFilterSection(state, ref) {
    if (state && state.parameters) {
        const defaultCountry = state.selectedCountryId ? state.selectedCountryId : state.parameters.countries[0];
        const defaultItemType = state.selectedItemTypeId ? state.selectedItemTypeId : state.parameters.itemTypes[0];
        const defaultSalesChannel = state.selectedSalesChannelId ? state.selectedSalesChannelId : state.parameters.salesChannels[0];
        const defaultRegion = state.selectedRegionId ? state.selectedRegionId : state.parameters.regions[0];
        const defaultOrderPriority = state.selectedOrderPriorityId ? state.selectedOrderPriorityId : state.parameters.orderPriorities[0];
        return <div className="filterSection">
            <div>
                <label>Region</label>
                <Dropdown onChange={ref.onRegionChange} options={state.parameters.regions} value={defaultRegion} placeholder="Select an option" />
            </div>
            <div>
                <label>Country</label>
                <Dropdown onChange={ref.onCountryChange} options={state.parameters.countries} value={defaultCountry} placeholder="Select an option" />
            </div>
            <div>
                <label>Item Type</label>
                <Dropdown onChange={ref.onItemTypeChange} options={state.parameters.itemTypes} value={defaultItemType} placeholder="Select an option" />
            </div>
            <div>
                <label>Sales Channel</label>
                <Dropdown onChange={ref.onSalesChannelChange} options={state.parameters.salesChannels} value={defaultSalesChannel} placeholder="Select an option" />
            </div>
            <div>
                <label>Order Priority</label>
                <Dropdown onChange={ref.onOrderPriorityChange} options={state.parameters.orderPriorities} value={defaultOrderPriority} placeholder="Select an option" />
            </div>
            <div className="filterButton">
                <button onClick={ref.getSales}>Filter</button>
            </div>
            <div className="saveChanges">
                <button onClick={ref.saveChanges}>Save Changes</button>
            </div>
        </div>
    }
}

function renderSalesTable(state) {
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
