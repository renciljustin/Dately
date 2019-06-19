export interface TokenPayload {
    name_id: string;
    unique_name: string;
    email: string;
    role: string;
    exp: Date;
    iss: string;
    aud: string;
}
