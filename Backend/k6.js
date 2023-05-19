import http from "k6/http";
import {sleep} from "k6";

export const options = {
    vus: 1000,
    duration: "20s",
};

export default function () {
    http.get("http://localhost/api/car-types?start-date=2000-01-01&end-date=2000-02-02&available-only=false");
    sleep(1);
}