import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dropdown from 'react-dropdown';
import 'react-dropdown/style.css';

class FetchData extends Component {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.onCountryChange = this.onCountryChange.bind(this);
        this.onRegionChange = this.onRegionChange.bind(this);
        this.onSalesChannelChange = this.onSalesChannelChange.bind(this);
        this.onItemTypeChange = this.onItemTypeChange.bind(this);
        this.onOrderPriorityChange = this.onOrderPriorityChange.bind(this);
        this.getSales = this.getSales.bind(this);
        this.onFilterButtonClick = this.onFilterButtonClick.bind(this);
        this.saveChanges = this.saveChanges.bind(this);
        this.nextPage = this.nextPage.bind(this);
        this.previousPage = this.previousPage.bind(this);
    }

    componentDidMount() {
        this.getSales(1);
        this.getParameters();
        this.getLastPageIndex(1);
    }

    componentWillReceiveProps() {

    }

    getFilter(pageIndex) {
        var filter = {};
        if (this.state) {
            filter.CountryId = this.state.selectedCountryId;
            filter.RegionId = this.state.selectedRegionId;
            filter.SalesChannelId = this.state.selectedSalesChannelId;
            filter.OrderPriorityId = this.state.selectedOrderPriorityId;
            filter.ItemTypeId = this.state.selectedItemTypeId;
        }
        filter.pageIndex = pageIndex;
        this.setState({ pageIndex: pageIndex });
        filter.PageSize = 1000;
        return filter;
    }

    getSales(pageIndex) {
        var filter = this.getFilter(pageIndex);
        fetch("http://localhost:5000/api/sales", {
            method: "POST", body: JSON.stringify(filter), headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ sales: result });
                }
            ).catch(error => console.error(error));
    }

    getLastPageIndex(pageIndex) {
        var filter = this.getFilter(pageIndex);
        fetch("http://localhost:5000/api/sales/getlastpageIndex", {
            method: "POST", body: JSON.stringify(filter), headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ lastPageIndex: result });
                }
            ).catch(error => console.error(error));
    }

    getParameters() {
        var filter = this.getFilter(1);
        fetch("http://localhost:5000/api/sales/getfilterparameters", {
            method: "POST", body: JSON.stringify(filter), headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ parameters: result });

                }
            ).catch(error => console.error(error));
    }

    onFilterButtonClick() {
        var pageIndex = 1;
        this.getSales(pageIndex);
        this.getLastPageIndex(pageIndex);
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

    nextPage() {
        if (this.state.pageIndex) {
            var pageIndex = this.state.pageIndex;
            pageIndex += 1;
            if (this.state.lastPageIndex >= pageIndex) {
                this.setState({ pageIndex: pageIndex });
                this.getSales(pageIndex);
            }
        }
    }

    previousPage() {
        if (this.state.pageIndex) {
            var pageIndex = this.state.pageIndex;
            if (pageIndex !== 1) {
                pageIndex -= 1;
                this.setState({ pageIndex: pageIndex });
                this.getSales(pageIndex);
            }
        }
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
            ).catch(error => console.error(error));
    }

    render() {
        return (
            <div>
                {renderFilterSection(this.state, this)}
                <h1>LinnWorks Sales</h1>
                {renderSalesTable(this.state)}
                {renderPagination(this)}
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
            <p className="clearfix">
                    <button className='btn btn-default pull-left' onClick={ref.onFilterButtonClick}>Filter</button>
            </p>
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
                            <td>{sale.region.regionName}</td>
                            <td>{sale.country.countryName}</td>
                            <td>{sale.itemType.itemTypeName}</td>
                            <td>{sale.salesChannel.salesChannelName}</td>
                            <td>{sale.orderPriority.orderPriorityName}</td>
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
    return <p className='clearfix text-center'>
        <button className='btn btn-default pull-left' onClick={props.previousPage}>Previous</button>
        <button className='btn btn-default pull-right' onClick={props.nextPage}>Next</button>
    </p>;
}

export default connect(

)(FetchData);
