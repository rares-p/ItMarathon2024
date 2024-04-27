import {Column, Entity, PrimaryColumn} from "typeorm";

@Entity({
    name: "preferences"
})
export class PreferenceEntity {

    @PrimaryColumn()
    studentId: string;

    @PrimaryColumn()
    slot: number;

    @Column()
    subjectId: string;
}