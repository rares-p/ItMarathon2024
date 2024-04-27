import {Column, Entity, PrimaryColumn} from "typeorm";
import {UUID} from "../../Utils/Types";

@Entity({
    name: "preferences"
})
export class PreferenceEntity {

    @PrimaryColumn()
    studentId: UUID;

    @PrimaryColumn()
    slot: number;

    @Column()
    subjectId: UUID;
}