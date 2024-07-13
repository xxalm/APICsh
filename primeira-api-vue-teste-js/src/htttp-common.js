import Axios from 'axios';

const createAxios = Axios.create({
    baseURL: "https://localhost7283"
});

export default createAxios;