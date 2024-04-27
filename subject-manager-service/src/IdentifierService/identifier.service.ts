import {forwardRef, Inject, Injectable} from "@nestjs/common";
import {InjectRepository} from "@nestjs/typeorm";
import {Repository} from 'typeorm';
import {IdentifierEntity} from "./entities/indentifier.entity";
import {UserRole} from "../UserService/entities/user.entity";
import {UserService} from "../UserService/user.service";
import {Years} from "../UserService/entities/student.entity";

@Injectable()
export class IdentifierService {
    constructor(
        @Inject(forwardRef(() => UserService))
        private readonly userService: UserService,
        @InjectRepository(IdentifierEntity)
        private readonly identifierRepository: Repository<IdentifierEntity>,
    ) {
    }

    async create(role: UserRole, name?: string,
                 credits?: number, grade?: number, year?: Years) {
        try {
            const identifier = this.identifierRepository.create({
                role: role
            });

            if (role == UserRole.STUDENT) {
                if (name == undefined || credits == undefined || grade == undefined || year == undefined)
                    return "Missing fields for creating a student account!";

                await this.userService.createStudent(name, credits, grade, year);
            }
            await this.identifierRepository.save(identifier);

            return {
                identifier: identifier.value
            };
        } catch (err) {
            return undefined;
        }
    }

    async getOne(value: string) {
        try {
            return await this.identifierRepository.findOneBy({value: value});
        } catch (err) {
            return undefined;
        }
    }

    async getAll() {
        try {
            return await this.identifierRepository.findBy({isUsed: false});
        } catch (err) {
            return undefined;
        }
    }

    async use(value: string) {
        try {
            const identifier = await this.identifierRepository.findOneBy({
                value: value,
            });

            if (identifier.isUsed == true)
                return false

            identifier.isUsed = true

            await this.identifierRepository.save(identifier);

            return identifier;
        } catch (err) {
            return undefined;
        }
    }

}