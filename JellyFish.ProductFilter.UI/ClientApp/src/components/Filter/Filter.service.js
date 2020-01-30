import axios from 'axios';

export const service = {

    getProducts: async (url, setProducts) => {
        try {
            axios.get(url)
                .then(res => {
                    setProducts(res.data);
                })
                .catch((error) => {
                    if (error.response && error.response.data) {
                        setProducts([]);
                    }                   
                });
        }
        catch (err) {
            alert(err);
        }
    }

}