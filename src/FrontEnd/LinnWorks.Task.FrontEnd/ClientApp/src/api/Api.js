export class Api {
    constructor(url) {
        this.baseApiUrl = url || "http://localhost:5000/api/";
    }

    async Get(url) {
        try {
            let response = await fetch(this.baseApiUrl.concat(url), {
                method: "GET", headers
            });
            return this.prepareResponse(response);
        } catch (e) {
            console.log(e);
        }
    }

    async Post(url, body) {
        try {
            let response = await fetch(this.baseApiUrl.concat(url), {
                method: "POST", body: body, mode: "no-cors", headers
            });
            return this.prepareResponse(response);
        } catch (e) {
            var ex = e;
            alert(ex);
        }
    }

    async Put(url, body) {
        try {
            let response = await fetch(this.baseApiUrl.concat(url), {
                method: "PUT", body: body, headers
            });
            return this.prepareResponse(response);
        } catch (e) {
            alert(e);
        }
    }

    prepareResponse(response) {
        if (response.ok || (response.status === 0 && response.type === "opaque")) {
            return response.body !== null ? response.json() : true;
        }
        else {
            return undefined;
        }
    }
}

const headers = { 'Accept': 'application/json', 'Content-Type': 'application/json' };