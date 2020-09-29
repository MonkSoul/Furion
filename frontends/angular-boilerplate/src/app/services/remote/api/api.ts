export * from './person.service';
import { PersonService } from './person.service';
export * from './personRelationship.service';
import { PersonRelationshipService } from './personRelationship.service';
export const APIS = [PersonService, PersonRelationshipService];
