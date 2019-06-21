import { ObjectQuery } from './object.query';

export interface UserQuery extends ObjectQuery {
    name?: string;
    gender?: number;
}
