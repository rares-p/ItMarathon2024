import {Injectable} from "@nestjs/common";
import {InjectRepository} from "@nestjs/typeorm";
import {Repository} from 'typeorm';
import {UserEntity, UserRole} from "./user.entity";

@Injectable()
export class UserService {
    constructor(
        @InjectRepository(UserEntity)
        private readonly userRepository: Repository<UserEntity>,
    ) {}

    async create(username: string, password: string) {
        let identifier = "";
        for (let i = 0; i < 32; ++i) {
            identifier += Math.floor(Math.random() * 10).toString()
        }

        const user = this.userRepository.create({
            username: username,
            password: password,
            identifier: identifier,
            role: UserRole.NORMAL
        })

        await this.userRepository.save(user);

        return {
            id: user.id,
            identifier: user.identifier
        }
    }

    async login(username: string, password: string) {
        try {
            const user = await this.userRepository.findOne({
                where: {
                    username: username,
                    password: password
                }
            })

            return {
                id: user.id,
                identifier: user.identifier,
                role: user.role
            };
        } catch (err) {
            return undefined;
        }
    }
}