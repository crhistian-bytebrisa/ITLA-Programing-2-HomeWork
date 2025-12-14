import {API_BASE_URL, AdaptResponse, getHeaders} from './base'

export const API = {
  
  GetAll: async (url) => {
    console.log("Aca se envia el get")
    const response = await fetch(`${API_BASE_URL}/${url}`, {
      method: 'GET',
      headers: getHeaders(),
      credentials : 'include'
    });
    console.log("Aca se envia la respuesta")
    console.log(response)
    return AdaptResponse(response);
  },

  GetById: async (url,id) =>{
    console.log("Aca se envia el get")
    const response = await fetch(`${API_BASE_URL}/${url}/${id}`, {
      method: 'GET',
      headers: getHeaders(),
      credentials : 'include'
    });
    console.log("Aca se envia la respuesta")
    console.log(response)
    return AdaptResponse(response);
  },

  Post: async (url, data) =>{
    console.log("Post de la api por " + `${url}`)
    console.log(JSON.stringify(data))
    const response = await fetch(`${API_BASE_URL}/${url}`, {
      method: 'POST',
      headers: getHeaders(),
      credentials : 'include',
      body: JSON.stringify(data)
    });
    console.log("Aca se envia la respuesta")
    console.log(response)
    return AdaptResponse(response);
  },

  Put: async (url, data) =>{
    console.log("Put de la api por " + `${url}`)
    console.log(JSON.stringify(data))
    const response = await fetch(`${API_BASE_URL}/${url}`, {
      method: 'PUT',
      headers: getHeaders(),
      credentials : 'include',
      body: JSON.stringify(data)
    });
    console.log("Aca se envia la respuesta")
    console.log(response)
    return AdaptResponse(response);
  },
  
  Delete: async (url) => {
    console.log("Delete de la api por " + `${url}`)
    const response = await fetch(`${API_BASE_URL}/${url}`, {
      method: 'DELETE',
      headers: getHeaders(),
      credentials : 'include'
    });
    console.log("Aca se envia la respuesta")
    console.log(response)
    return AdaptResponse(response);
    }
}