import {Module} from "@nestjs/common";
import {TypeOrmModule} from "@nestjs/typeorm";
import {IdentifierEntity} from "./entities/indentifier.entity";
import {IdentifierService} from "./identifier.service";
import {IdentifierController} from "./identifier.controller";
import {UserService} from "../UserService/user.service";
import {UserEntity} from "../UserService/entities/user.entity";
import {StudentEntity} from "../UserService/entities/student.entity";

@Module({
    imports: [TypeOrmModule.forFeature([IdentifierEntity, UserEntity, StudentEntity])],
    controllers: [IdentifierController],
    exports: [IdentifierService],
    providers: [IdentifierService, UserService]
})
export class IdentifierModule{}