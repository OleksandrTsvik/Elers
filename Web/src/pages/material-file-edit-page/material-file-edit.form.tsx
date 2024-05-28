import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useUpdateCourseMaterialFileMutation } from '../../api/course-materials.api';
import { FormMode } from '../../common/types';
import {
  CourseTabType,
  MaterialFileForm,
  MaterialFileSubmitValues,
  getCourseEditPagePath,
} from '../../shared';

interface Props {
  courseId: string;
  courseMaterialId: string;
  tabId: string;
  courseTabType: CourseTabType;
  fileTitle: string;
}

export default function MaterialFileEditForm({
  courseId,
  courseMaterialId,
  tabId,
  courseTabType,
  fileTitle,
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [updateCourseMaterial, { isLoading, error }] =
    useUpdateCourseMaterialFileMutation();

  const handleSubmit = async (values: MaterialFileSubmitValues) => {
    await updateCourseMaterial({ id: courseMaterialId, ...values })
      .unwrap()
      .then(() =>
        navigate(getCourseEditPagePath(courseId, tabId, courseTabType)),
      );
  };

  return (
    <MaterialFileForm
      mode={FormMode.Edit}
      initialValues={{ title: fileTitle, files: [] }}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
