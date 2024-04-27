import {Module} from "@nestjs/common";
import {TypeOrmModule} from "@nestjs/typeorm";
import {UserController} from "./user.controller";
import {UserService} from "./user.service";
import {UserEntity} from "./entities/user.entity";
import {StudentEntity} from "./entities/student.entity";
import {IdentifierService} from "../IdentifierService/identifier.service";
import {IdentifierEntity} from "../IdentifierService/entities/indentifier.entity";

@Module({
    imports: [TypeOrmModule.forFeature([UserEntity, StudentEntity, IdentifierEntity])],
    controllers: [UserController],
    exports: [UserService],
    providers: [UserService, IdentifierService]
})
export class UserModule{}