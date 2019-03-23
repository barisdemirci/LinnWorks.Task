import { Api } from './api';

export class QueueApi extends Api {

    constructor(props) {
        super(props);
        const baseApiUrl = "http://linnworksqueuemicroservice-dev.eu-central-1.elasticbeanstalk.com/api/";
        this.state = { baseApiUrl: baseApiUrl };
    }
}