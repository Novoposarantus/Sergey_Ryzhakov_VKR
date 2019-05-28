<template>
    <v-dialog
        v-model="dialog"
        width="300"
    >
        <template v-slot:activator="{ on }">
            <v-btn  v-on="on"
                    color="primary" 
                    dark>
                Добавить тип вопроса
            </v-btn>
        </template>

        <v-card>
            <v-form ref="form"
                    v-model="valid"
                    lazy-validation>
                <v-card-title
                    class="headline grey lighten-2"
                    primary-title
                >
                    Напишите имя типа
                </v-card-title>

                <v-card-text>
                    <v-text-field
                        v-model="name"
                        :rules="[v => !!v || 'Поле не моет быть пустым']"
                        label="Имя"
                        required
                    ></v-text-field>
                </v-card-text>

                <v-divider></v-divider>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                    color="primary"
                    flat
                    @click="save()"
                    >
                    Сохранить
                    </v-btn>
                </v-card-actions>
            </v-form>
        </v-card>
    </v-dialog>
</template>

<script>
import {mapActions} from 'vuex';
export default {
    data(){
        return{
            dialog: false,
            name: "",
            valid: false
        }
    },
    methods:{
        ...mapActions({
            saveType: "questionEdit/SAVE_TYPE"
        }),
        async save(){
            if (!this.$refs.form.validate()) {
                return;
            }
            await this.saveType(this.name);
            this.dialog = false;
        }
    }
}
</script>
