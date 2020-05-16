export interface IPerson {
    id: number;
    firstName: string;
    lastName: string;
    dob: Date;
    dod?: Date;
    avatarUrl?: string;
    interests?: string;

    street1: string;
    street2: string;
    city: string;
    state: string; // I know... USA-centric.  Real app would have more flexible schema
    postalCode: string;
}
