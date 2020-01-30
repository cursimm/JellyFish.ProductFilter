import React, { useEffect, useState, Fragment } from 'react';
import { useForm } from 'react-hook-form';
import { config } from './Filter.config.js';
import { service } from './Filter.service.js'
import './Filter.css';

export default function Filter() {

    const [products, setProducts] = useState([]);
    const [url] = useState(config.url);

    const { register, handleSubmit, errors } = useForm();
    const onSubmit = data => {
        service.getProducts(url + '/' + data.filterPrice, setProducts);
    }

    let content;

    if (products.length === 0) {
        content = <div id='no-result'>
            No results found
            </div>;
    } else {
        content = <div > {
            products.map(item => (
                <div id='detail' key={item.id}>
                    <div className='detail-left'>
                        <img src={item.image} alt={item.name} height='150' width='150' />
                    </div>
                    <div className='detail-right'>
                        <label id='lblName'>{item.name}</label>
                        <label id='lblPrice'>&pound;{item.price}</label>
                        <p>{item.description}</p>
                    </div>
                </div>
            ))
        }</div>;
    }

    useEffect(() => {
        service.getProducts(url, setProducts);
    }, []);

    return (
        <Fragment>
            <body>
                <div className='wrapper'>
                    <div id="header"><h3>Products</h3></div>
                    <div id='content'>
                        <div id='content-left'>
                            <form onSubmit={handleSubmit(onSubmit)}>
                                <p>
                                    Max Price: <input maxLength='10' name='filterPrice' id='filterPrice' type='text'
                                        ref={register({ required: true, pattern: /\d+/ })} />
                                    {errors.filterPrice && 'A price is required.'}
                                    <button type='submit'>Refine search</button>
                                </p>
                            </form>
                        </div>
                        <div id='content-main'>
                            {content}
                        </div>
                    </div>
                    <div id="footer"></div>
                </div>
            </body>
        </Fragment>
    );
}


