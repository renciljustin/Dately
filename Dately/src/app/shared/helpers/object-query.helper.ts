import { ObjectQuery } from 'src/app/core/queries/object.query';

export class ObjectQueryHelper<T extends ObjectQuery> {

    mapObjectQuery(src: any, dest: T) {
        for (const key in src) {
            if (dest.hasOwnProperty(key)) {
                dest[key] = src[key];
            } else {
                dest[key] = src[key];
            }
        }
        return dest;
    }
}
