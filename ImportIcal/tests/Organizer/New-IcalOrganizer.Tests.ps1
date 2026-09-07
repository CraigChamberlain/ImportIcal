#Requires -Module Pester

Describe 'New-IcalOrganizer' {
    It 'Given all parameters' {
              $org = New-IcalOrganizer `
                        -SentBy "mailto:bob@example" `
                        -CommonName Bob `
                        -DirectoryEntry "http://www.resouce.com/thing" `
                        -Language "En" `
                        -Value "mailto:bob@example2" 

            $org.SentBy | Should -Be "mailto:bob@example" 
            $org.CommonName | Should -Be "Bob" 
            $org.DirectoryEntry | Should -Be "http://www.resouce.com/thing" 
            $org.Language | Should -Be "En"
            $org.Value | Should -Be "mailto:bob@example2" 
    }
     It 'Given minimum parameters' {

        $org = New-IcalOrganizer -Value "mailto:bob@example2" 

        $org.SentBy | Should -BeNullOrEmpty 
        $org.CommonName | Should -BeNullOrEmpty
        $org.DirectoryEntry | Should -BeNullOrEmpty
        $org.Language | Should -BeNullOrEmpty
        $org.Value | Should -Be "mailto:bob@example2" 

    }
}