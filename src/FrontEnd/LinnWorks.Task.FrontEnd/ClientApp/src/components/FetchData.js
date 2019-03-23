import React, { Component } from 'react';
import { connect } from 'react-redux';
import Dropdown from 'react-dropdown';
import 'react-dropdown/style.css';
import 'react-table/react-table.css';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

class FetchData extends Component {
    componentWillMount() {
        this.onCountryChange = this.onCountryChange.bind(this);
        this.onRegionChange = this.onRegionChange.bind(this);
        this.onSalesChannelChange = this.onSalesChannelChange.bind(this);
        this.onItemTypeChange = this.onItemTypeChange.bind(this);
        this.onOrderPriorityChange = this.onOrderPriorityChange.bind(this);
        this.onOrderDateChange = this.onOrderDateChange.bind(this);
        this.onOrderIdChange = this.onOrderIdChange.bind(this);
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

    getFilterQuery = (pageIndex) => {
        var filter = {};
        if (this.state) {
            filter.CountryId = this.state.selectedCountryId;
            filter.RegionId = this.state.selectedRegionId;
            filter.SalesChannelId = this.state.selectedSalesChannelId;
            filter.OrderPriorityId = this.state.selectedOrderPriorityId;
            filter.ItemTypeId = this.state.selectedItemTypeId;
            filter.OrderDate = this.state.selectedOrderDate !== null ? this.state.selectedOrderDate : undefined;
            filter.OrderId = this.state.selectedOrderId !== "" ? this.state.selectedOrderId : undefined;
        }
        filter.PageIndex = pageIndex;
        this.setState({ pageIndex: pageIndex });
        filter.PageSize = 1000;

        let query = "";
        query = query.concat(`PageIndex=${filter.PageIndex}&PageSize=${filter.PageSize}`);
        if (filter.CountryId !== undefined) {
            query = query.concat(`&CountryId=${filter.CountryId}`);
        }
        if (filter.SalesChannelId !== undefined) {
            query = query.concat(`&SalesChannelId=${filter.SalesChannelId}`);
        }
        if (filter.OrderPriorityId !== undefined) {
            query = query.concat(`&OrderPriorityId=${filter.OrderPriorityId}`);
        }
        if (filter.RegionId !== undefined) {
            query = query.concat(`&RegionId=${filter.RegionId}`);
        }
        if (filter.ItemTypeId !== undefined) {
            query = query.concat(`&ItemTypeId=${filter.ItemTypeId}`);
        }
        if (filter.OrderDate !== undefined) {
            query = query.concat(`&OrderDate=${filter.OrderDate.toISOString()}`);
        }
        if (filter.OrderId) {
            query = query.concat(`&OrderId=${filter.OrderId}`);
        }
        return query;
    }

    getSales(pageIndex) {
        var query = this.getFilterQuery(pageIndex);
        var url = `http://localhost:5000/api/sales?${query}`;
        fetch(url, {
            method: "GET", headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ sales: result.length > 0 ? result : undefined });
                }
            ).catch(error => console.error(error));
    }

    getLastPageIndex(pageIndex) {
        var query = this.getFilterQuery(pageIndex);
        var url = `http://localhost:5000/api/sales/lastpageIndex?${query}`;
        fetch(url, {
            method: "GET", headers: {
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
        var query = this.getFilterQuery(1);
        var url = `http://localhost:5000/api/sales/filterparameters?${query}`;
        fetch(url, {
            method: "GET", headers: {
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

    onOrderDateChange(e) {
        this.setState({ selectedOrderDate: e });
    }

    onOrderIdChange(e) {
        this.setState({ selectedOrderId: e.currentTarget.value });
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
        if (this.state.editSaleList) {
            fetch("http://localhost:5000/api/sales", {
                method: "PUT", body: JSON.stringify(this.state.editSaleList), headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
                .done(function () {
                    this.setState({
                        editSaleList: undefined
                    });
                    alert("The changes are saved successfully.");
                }).fail(error => console.error(error));
        }
        else {
            alert("Nothing to save");
        }
    }

    onEditRegionClick(currentValue, saleId, ref) {
        var displayRegion = {};
        displayRegion.currentValue = currentValue;
        displayRegion.saleId = saleId;
        ref.setState({ displayRegion: displayRegion });
    }

    onEditCountryClick(currentValue, saleId, ref) {
        var displayCountry = {};
        displayCountry.currentValue = currentValue;
        displayCountry.saleId = saleId;
        ref.setState({ displayCountry: displayCountry });
    }

    onEditOrderDateChange(currentValue, saleId, ref) {
        var displayOrderDate = {};
        displayOrderDate.currentValue = currentValue;
        displayOrderDate.saleId = saleId;
        ref.setState({ displayOrderDate: displayOrderDate });
    }

    renderOrderDatePicker(ref, currentValue) {
        var date = new Date(currentValue);
        return <DatePicker selected={date} />;
    }

    onRenderRegionChange(sale, ref, e) {
        sale.region.regionId = e.value;
        sale.region.regionName = e.label;
        ref.AddSaleObjectToList(sale);
    }

    onRenderCountryChange(sale, ref, e) {
        sale.country.countryId = e.value;
        sale.country.countryName = e.label;
        ref.AddSaleObjectToList(sale);
    }

    AddSaleObjectToList(newSale) {
        var newList = this.state.editSaleList;
        if (newList === undefined) {
            newList = [];
            newList.push(newSale);
        }
        else {
            var index = newList.indexOf(newSale);
            if (index > -1) {
                newList.splice(index, 1);
            }
            newList.push(newSale);
        }

        this.setState({ editSaleList: newList });
    }

    render() {
        return (
            <div>
                {renderFilterSection(this.state, this)}
                <h1>LinnWorks Sales</h1>
                {renderSalesTable(this)}
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
        return <div>
            <div className="filterSection">
                <div className="region">
                    <label>Region</label>
                    <Dropdown onChange={ref.onRegionChange} options={state.parameters.regions} value={defaultRegion} placeholder="Select an option" />
                </div>
                <div className="country">
                    <label>Country</label>
                    <Dropdown onChange={ref.onCountryChange} options={state.parameters.countries} value={defaultCountry} placeholder="Select an option" />
                </div>
                <div className="itemType">
                    <label>Item Type</label>
                    <Dropdown onChange={ref.onItemTypeChange} options={state.parameters.itemTypes} value={defaultItemType} placeholder="Select an option" />
                </div>
                <div className="salesChannel">
                    <label>Sales Channel</label>
                    <Dropdown onChange={ref.onSalesChannelChange} options={state.parameters.salesChannels} value={defaultSalesChannel} placeholder="Select an option" />
                </div>
                <div className="orderPriority">
                    <label>Order Priority</label>
                    <Dropdown onChange={ref.onOrderPriorityChange} options={state.parameters.orderPriorities} value={defaultOrderPriority} placeholder="Select an option" />
                </div>
                <div className="orderDate">
                    <label>Order Date</label>
                    <div>
                        <DatePicker selected={ref.state.selectedOrderDate} onChange={ref.onOrderDateChange} />
                    </div>
                </div>
                <div className="orderId">
                    <label>Order Id</label>
                    <div>
                        <input type='number' onChange={ref.onOrderIdChange.bind(this)} />
                    </div>
                </div>
            </div>
            <div>
                <button className='btn btn-default pull-right saveButton' onClick={ref.saveChanges}>Save Changes</button>
                <button className='btn btn-default pull-right filterButton' onClick={ref.onFilterButtonClick}>Filter</button>
            </div>
        </div>
    }
}

function renderRegionDropDown(ref, currentValue, sale) {
    return <Dropdown onChange={ref.onRenderRegionChange.bind(this, sale, ref)} options={ref.state.parameters.regions} value={currentValue.toString()} />;
}

function renderCountryDropDown(ref, currentValue, sale) {
    return <Dropdown onChange={ref.onRenderCountryChange.bind(this, sale, ref)} options={ref.state.parameters.countries} value={currentValue.toString()} />;
}

function renderSalesTable(ref) {
    if (ref.state && ref.state.sales && ref.state.parameters) {
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
                    {ref.state.sales.map(sale =>
                        <tr key={sale.saleId}>
                            <td>{sale.saleId}</td>
                            <td onClick={ref.onEditRegionClick.bind(this, sale.region.regionId, sale.saleId, ref)} >
                                {ref.state.displayRegion === undefined ? sale.region.regionName : ref.state.displayRegion.saleId === sale.saleId ?
                                    renderRegionDropDown(ref, sale.region.regionId, sale) : sale.region.regionName}
                            </td>
                            <td onClick={ref.onEditCountryClick.bind(this, sale.country.countryId, sale.saleId, ref)} >
                                {ref.state.displayCountry === undefined ? sale.country.countryName : ref.state.displayCountry.saleId === sale.saleId ?
                                    renderCountryDropDown(ref, sale.country.countryId, sale) : sale.country.countryName}</td>
                            <td>{sale.itemType.itemTypeName}</td>
                            <td>{sale.salesChannel.salesChannelName}</td>
                            <td>{sale.orderPriority.orderPriorityName}</td>
                            <td onClick={ref.onEditOrderDateChange.bind(this, sale.orderDate, sale.saleId, ref)}>
                                {ref.state.displayOrderDate === undefined ? sale.orderDate : ref.state.displayOrderDate.saleId === sale.saleId ?
                                    ref.renderOrderDatePicker(ref, sale.orderDate) : sale.orderDate}
                            </td>
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
}

function renderPagination(props) {
    return <p className='clearfix text-center'>
        <button className='btn btn-default pull-left' onClick={props.previousPage}>Previous</button>
        <button className='btn btn-default pull-right' onClick={props.nextPage}>Next</button>
    </p>;
}

export default connect(

)(FetchData);
