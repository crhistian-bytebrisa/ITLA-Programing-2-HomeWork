export const API_BASE_URL = 'https://mediagenda.somee.com/api';

export const AdaptResponse = async (response) => {

  //en caso de que sea un 204 y no tenga un tipo de contenido retorna null
 if (response.status === 204 || response.headers.get('content-length') === '0') {
    return null;
  }
  
  // Se consigue el tipo del congenido
  const contentType = response.headers.get('content-type');

  // En caso de que en el tipo de contenido este que es json retorna el json
  if (contentType && contentType.includes('application/json')) {
    return response.json();
  }
  
  // Retorna text en caso de que no sea un json type
  return response.text();
};

export const getHeaders = () => ({
  'Content-Type': 'application/json',
});