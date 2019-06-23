export function serializeQueryParams(queryParams: any): string {
    const queryParamsArr: any[] = [];

    for (const key in queryParams) {
        if (queryParams[key] !== null || queryParams[key] !== undefined) {
            queryParamsArr.push(`${key}=${queryParams[key]}`);
        }
    }
    return queryParamsArr.join('&');
}
