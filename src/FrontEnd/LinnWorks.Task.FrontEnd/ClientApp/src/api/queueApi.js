import { Api } from './api';

export class QueueApi extends Api {

    constructor(url) {
        const baseApiUrl = url || "http://linnworksqueuemicroservice-dev.eu-central-1.elasticbeanstalk.com/api/";
        super(baseApiUrl);
    }
}