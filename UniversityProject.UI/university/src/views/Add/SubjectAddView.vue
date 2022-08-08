<template>

    <div>
        <SubjectAdd></SubjectAdd>
    </div>
    

</template>

<script>
import SubjectAdd from '../../components/Add/SubjectAddComponent.vue'
export default{
    name: 'SubjectViewAdd',
    props:{
        msg:String
    },
    components: {
        SubjectAdd
    }
}
</script>

