import React, { Component } from 'react';
import Dropdown from 'react-dropdown';
import 'react-dropdown/style.css';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { Api } from '../api/Api';
import { TD } from './td';

export class FetchData extends Component {
    async componentDidMount() {
        this.api = new Api();
        this.getSales(1);
        this.getParameters();
        this.getLastPageIndex(1);
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

    async getSales(pageIndex: number) {
        var query = this.getFilterQuery(pageIndex);
        var url = `sales?${query}`;
        var sales = await this.api.Get(url);
        if (sales !== undefined) {
            this.setState({ sales: sales.length > 0 ? sales : undefined });
        }
    }

    async getLastPageIndex(pageIndex) {
        var query = this.getFilterQuery(pageIndex);
        var url = `sales/lastpageIndex?${query}`;
        var lastPageIndex = await this.api.Get(url);
        this.setState({ lastPageIndex: lastPageIndex });
    }

    async getParameters() {
        var query = this.getFilterQuery(1);
        var url = `sales/filterparameters?${query}`;
        var parameters = await this.api.Get(url);
        this.setState({ parameters: parameters });
    }

    onFilterButtonClick = () => {
        var pageIndex = 1;
        this.getSales(pageIndex);
        this.getLastPageIndex(pageIndex);
    }

    async saveChanges(e) {
        e.preventDefault();
        if (this.state.editSaleList) {
            await this.api.Put("sales", JSON.stringify(this.state.editSaleList));
            this.setState({ editSaleList: undefined });
            alert("The changes were saved.");
        }
        else {
            alert("Nothing to save");
        }
    }

    onFieldChange = (fieldName, e) => {
        let value = e !== null ? e.value : '';
        if (!isNaN(new Date(e).valueOf())) {
            value = e;
        }
        else if (e.currentTarget !== undefined) {
            value = e.currentTarget.value;
        }
        this.setState({ [fieldName]: value });
    }

    nextPage = () => {
        if (this.state.pageIndex) {
            var pageIndex = this.state.pageIndex;
            pageIndex += 1;
            if (this.state.lastPageIndex >= pageIndex) {
                this.setState({ pageIndex: pageIndex });
                this.getSales(pageIndex);
            }
        }
    }

    previousPage = () => {
        if (this.state.pageIndex) {
            var pageIndex = this.state.pageIndex;
            if (pageIndex !== 1) {
                pageIndex -= 1;
                this.setState({ pageIndex: pageIndex });
                this.getSales(pageIndex);
            }
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

    onEditRegionChange(sale, ref, e) {
        sale.region.regionId = e.value;
        sale.region.regionName = e.label;
        ref.AddSaleObjectToList(sale);
        ref.setState({ displayRegion: undefined });
    }

    onEditCountryChange(sale, ref, e) {
        sale.country.countryId = e.value;
        sale.country.countryName = e.label;
        ref.AddSaleObjectToList(sale);
        ref.setState({ displayCountry: undefined });
    }

    addSaleObjectToList(newSale) {
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

    commitData(newValue, newValueKeyFieldName, saleId) {
        let sale = this.state.sales.filter(x => x.saleId === saleId)[0];
        sale[newValueKeyFieldName] = newValue;
        this.setState({ sales: this.state.sales });
        this.addSaleObjectToList(sale);
    }

    renderSalesTable = () => {
        if (this.state && this.state.sales && this.state.parameters) {
            const regions = this.state.parameters.regions;
            const countries = this.state.parameters.countries;
            const itemTypes = this.state.parameters.itemTypes;
            const salesChannels = this.state.parameters.salesChannels;
            const orderPriorities = this.state.parameters.orderPriorities;

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
                        {this.state.sales.map(sale => (
                            <tr key={sale.saleId}>
                                <td> {sale.saleId} </td>
                                <TD editable type="select" options={regions} value={sale.regionId} commitData={(value) => this.commitData(value, "regionId", sale.saleId)} />
                                <TD editable type="select" options={countries} value={sale.countryId} commitData={(value) => this.commitData(value, "countryId", sale.saleId)} />
                                <TD editable type="select" options={itemTypes} value={sale.itemTypeId} commitData={(value) => this.commitData(value, "itemTypeId", sale.saleId)} />
                                <TD editable type="select" options={salesChannels} value={sale.salesChannelId} commitData={(value) => this.commitData(value, "salesChannelId", sale.saleId)} />
                                <TD editable type="select" options={orderPriorities} value={sale.orderPriorityId} commitData={(value) => this.commitData(value, "orderPriorityId", sale.saleId)} />
                                <TD editable type="date" value={sale.orderDate} commitData={(value) => this.commitData(value, "orderDate", sale.saleId)} />
                                <TD editable type="number" value={sale.orderID} commitData={(value) => this.commitData(value, "orderID", sale.saleId)} />
                                <TD editable type="date" value={sale.shipDate} commitData={(value) => this.commitData(value, "shipDate", sale.saleId)} />
                                <TD editable type="number" value={sale.unitSold} commitData={(value) => this.commitData(value, "unitSold", sale.saleId)} />
                                <TD editable type="number" value={sale.unitPrice} commitData={(value) => this.commitData(value, "unitPrice", sale.saleId)} />
                                <TD editable type="number" value={sale.unitCost} commitData={(value) => this.commitData(value, "unitCost", sale.saleId)} />
                                <TD editable type="number" value={sale.totalRevenue} commitData={(value) => this.commitData(value, "totalRevenue", sale.saleId)} />
                                <TD editable type="number" value={sale.totalCost} commitData={(value) => this.commitData(value, "totalCost", sale.saleId)} />
                                <TD editable type="number" value={sale.totalProfit} commitData={(value) => this.commitData(value, "totalProfit", sale.saleId)} />
                            </tr>)
                        )}
                    </tbody>
                </table>
            );
        }
    }

    render() {
        return (
            <div>
                {renderFilterSection(this.state, this)}
                <h1>LinnWorks Sales</h1>
                {this.renderSalesTable(this)}
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
                    <Dropdown onChange={ref.onFieldChange.bind(this, 'selectedRegionId')} options={state.parameters.regions} value={defaultRegion} placeholder="Select an option" />
                </div>
                <div className="country">
                    <label>Country</label>
                    <Dropdown onChange={ref.onFieldChange.bind(this, 'selectedCountryId')} options={state.parameters.countries} value={defaultCountry} placeholder="Select an option" />
                </div>
                <div className="itemType">
                    <label>Item Type</label>
                    <Dropdown onChange={ref.onFieldChange.bind(this, 'selectedItemTypeId')} options={state.parameters.itemTypes} value={defaultItemType} placeholder="Select an option" />
                </div>
                <div className="salesChannel">
                    <label>Sales Channel</label>
                    <Dropdown onChange={ref.onFieldChange.bind(this, 'selectedSalesChannelId')} options={state.parameters.salesChannels} value={defaultSalesChannel} placeholder="Select an option" />
                </div>
                <div className="orderPriority">
                    <label>Order Priority</label>
                    <Dropdown onChange={ref.onFieldChange.bind(this, 'selectedOrderPriorityId')} options={state.parameters.orderPriorities} value={defaultOrderPriority} placeholder="Select an option" />
                </div>
                <div className="orderDate">
                    <label>Order Date</label>
                    <div>
                        <DatePicker selected={ref.state.selectedOrderDate} onChange={ref.onFieldChange.bind(this, 'selectedOrderDate')} />
                    </div>
                </div>
                <div className="orderId">
                    <label>Order Id</label>
                    <div>
                        <input type='number' onChange={ref.onFieldChange.bind(this, 'selectedOrderId')} />
                    </div>
                </div>
            </div>
            <div>
                <button className='btn btn-default pull-right saveButton' onClick={ref.saveChanges.bind(ref)}>Save Changes</button>
                <button className='btn btn-default pull-right filterButton' onClick={ref.onFilterButtonClick}>Filter</button>
            </div>
        </div>
    }
}

function renderPagination(props) {
    return <p className='clearfix text-center'>
        <button className='btn btn-default pull-left' onClick={props.previousPage}>Previous</button>
        <button className='btn btn-default pull-right' onClick={props.nextPage}>Next</button>
    </p>;
}