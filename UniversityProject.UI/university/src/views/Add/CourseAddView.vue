<template>

    <CourseAdd></CourseAdd>

</template>

<script>
import CourseAdd from '../../components/Add/CourseAddComponent.vue'
export default{
    name: 'CourseViewAdd',
    props:{
        msg:String
    },
    components: {
        CourseAdd
    }
}
</script>