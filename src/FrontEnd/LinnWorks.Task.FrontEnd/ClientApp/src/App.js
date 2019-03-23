import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import ImportFile from './components/ImportFile';
import FetchData from './components/FetchData';

export default () => (
    <Layout>
        <Route exact path='/' component={ImportFile} />
        <Route path='/fetchdata' component={FetchData} />
    </Layout>
);
