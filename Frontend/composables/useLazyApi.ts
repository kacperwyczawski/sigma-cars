import {Ref} from "vue";

export default function (url: string | Request | (Ref<string | Request>) | (() => string | Request), options = {}) {
    return useLazyFetch(url, {baseURL: "http://localhost/api/", ...options});
}