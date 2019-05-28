import Cookie from 'js-cookie';
import axios from 'axios';

export async function request(url, type, data){
    console.log(url);
    const response = await axios({
        url,
        method: type,
        data: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json',
            'Authorization': "Bearer " + Cookie.get('user-token')
        }
    })
    return {response, json: response.data};

}