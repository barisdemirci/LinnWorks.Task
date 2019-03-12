import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import ImportFile from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';

export default () => (
    <Layout>
        <Route exact path='/' component={ImportFile} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
    </Layout>
);
