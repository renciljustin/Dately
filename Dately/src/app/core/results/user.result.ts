import { UserList } from '../models/user-list.model';
export interface UserResult {
    users: UserList[];
    total: number;
}
