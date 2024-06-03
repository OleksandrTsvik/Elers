import { Button, Space, Typography } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { TextEditorOutput } from '../../../common/typography';
import { CourseMaterial } from '../../../models/course-material.type';
import {
  CourseMaterialFileIcon,
  CourseMaterialIcon,
  CourseMaterialType,
  useDownloadCourseMaterialFile,
} from '../../../shared';
import { DATE_FORMAT } from '../../../utils/constants/app.constants';

interface Props {
  material: CourseMaterial;
}

export default function CourseTabMaterial({ material }: Props) {
  const { t } = useTranslation();
  const { downloadCourseMaterialFile } = useDownloadCourseMaterialFile();

  switch (material.type) {
    case CourseMaterialType.Content:
      return <TextEditorOutput text={material.content} />;
    case CourseMaterialType.Link:
      return (
        <Link className="d-block" to={material.link} target="_blank">
          <Space align="start">
            <CourseMaterialIcon type={CourseMaterialType.Link} />
            {material.title}
          </Space>
        </Link>
      );
    case CourseMaterialType.File:
      return (
        <Button
          className="d-block p-0"
          type="link"
          icon={<CourseMaterialFileIcon extension={material.fileName} />}
          onClick={() =>
            downloadCourseMaterialFile(
              material.uniqueFileName,
              material.fileName,
            )
          }
        >
          {material.title}
        </Button>
      );
    case CourseMaterialType.Assignment:
      return (
        <div className="mt-field">
          <Link className="d-block" to={''}>
            <Space align="start">
              <CourseMaterialIcon type={CourseMaterialType.Assignment} />
              {material.title}
            </Space>
          </Link>
          <TextEditorOutput text={material.description} />
          <Typography.Paragraph className="m-0" type="secondary">
            {t('course_material.deadline')}:{' '}
            {material.deadline
              ? dayjs(material.deadline).format(DATE_FORMAT)
              : t('course_material.no_deadline')}
          </Typography.Paragraph>
        </div>
      );
  }
}
