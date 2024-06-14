import { ReloadOutlined } from '@ant-design/icons';
import { Button, Skeleton, Spin } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import CourseGradesHead from './course-grades.head';
import CourseGradesTable from './course-grades.table';
import AddColumnModalButton from './modals/add-column.modal-button';
import EditColumnModal from './modals/edit-column.modal';
import SaveGradeModal from './modals/save-grade.modal';
import { useGetCourseGradesQuery } from '../../api/grades.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function CourseGradesPage() {
  const { courseId } = useParams();
  const { t } = useTranslation();

  const { data, isLoading, isFetching, error, refetch } =
    useGetCourseGradesQuery({
      courseId,
    });

  if (isLoading) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <CourseGradesHead />

      <Spin spinning={isFetching}>
        <AddColumnModalButton courseId={courseId} />

        <CourseGradesTable data={data} />

        <Button
          className="right-btn mt-field"
          icon={<ReloadOutlined />}
          onClick={refetch}
        >
          {t('course_grades_page.update_table')}
        </Button>
      </Spin>

      <EditColumnModal />
      <SaveGradeModal />
    </>
  );
}
