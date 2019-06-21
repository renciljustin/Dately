export function authBearer() {
    return {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    };
}
